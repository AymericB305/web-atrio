import { Injectable, computed, inject, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject, map, tap } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

export interface GetAllPeopleResponse {
  people: GetPersonResponse[];
}

export interface GetPersonResponse {
  id: number;
  firstName: string;
  lastName: string;
  age: number;
  currentJobs: JobResponse[];
}

export interface JobResponse {
  companyName: string;
  position: string;
  startDate: string;
}

export interface AddJobResponse {
  id: number;
  companyName: string;
  position: string;
  startDate: string;
  endDate?: string | undefined;
}

@Injectable({
  providedIn: 'root'
})
export class PeopleService {

  private apiUrl = environment.peopleApi;
  private http = inject(HttpClient)

  private state = signal<GetPersonResponse[]>([])

  people = computed(() => this.state())

  load(): Observable<GetPersonResponse[]> {
    return this.http.get<GetAllPeopleResponse>(this.apiUrl).pipe(
      map(response => response.people),
      tap(people => this.state.set(people))
    )
  }

  add(personData: { firstName: string; lastName: string; birthDate: string }): Observable<GetPersonResponse> {
    return this.http.post<GetPersonResponse>(`${this.apiUrl}`, personData).pipe(
      tap(person => this.state.set([...this.state(), person]))
    )
  }

  addJobToPerson(personId: number, jobData: { companyName: string; position: string; startDate: string; endDate?: string | null }): Observable<AddJobResponse> {
    return this.http.post<AddJobResponse>(`${this.apiUrl}/${personId}/jobs`, jobData);
  }

  getByCompany(companyName: string): Observable<GetPersonResponse[]> {
    return this.http.get<GetAllPeopleResponse>(`${this.apiUrl}/by-company/${companyName}`).pipe(
      map(response => response.people),
    )
  }
}
