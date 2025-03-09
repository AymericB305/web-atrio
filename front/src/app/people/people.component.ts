import { Component, inject } from "@angular/core";
import { TableModule } from "primeng/table"
import { GetPersonResponse, PeopleService } from "./people.service";
import { takeUntilDestroyed } from "@angular/core/rxjs-interop";
import { DialogModule } from "primeng/dialog";
import { DatePipe } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { ButtonModule } from "primeng/button";
import { DatePickerModule } from "primeng/datepicker";

@Component({
  selector: 'app-people',
  imports: [
    TableModule,
    DialogModule,
    DatePickerModule,
    FormsModule,
    DatePipe,
    ButtonModule,
  ],
  providers: [
    DatePipe
  ],
  templateUrl: './people.component.html',
  styles: ``,
  standalone: true
})
export class PeopleComponent {

  private service = inject(PeopleService)
  private datePipe = inject(DatePipe)

  constructor() {
    this.service.load().pipe(takeUntilDestroyed()).subscribe();
  }

  people = this.service.people
  addPersonDialog = false;
  newPerson = {
    firstName: '',
    lastName: '',
    birthDate: null as Date | null,
  };

  addJobDialog = false;
  newJob = {
    companyName: '',
    position: '',
    startDate: null as Date | null,
    endDate: null as Date | null,
  };
  selectedPerson: any;

  getByCompanyDialog = false;
  company = {
    name: '',
  }
  getByCompanyResult: GetPersonResponse[] = [];

  addPerson() {
    if (this.newPerson.firstName && this.newPerson.lastName && this.newPerson.birthDate) {
      const personData = {
        firstName: this.newPerson.firstName,
        lastName: this.newPerson.lastName,
        birthDate: this.datePipe.transform(this.newPerson.birthDate, 'yyyy-MM-dd')!,
      };

      this.service.add(personData).subscribe(
        () => {
          this.addPersonDialog = false;
          this.newPerson = { firstName: '', lastName: '', birthDate: null };
        }
      );
    }
  }

  openAddJobDialog(person: any) {
    this.selectedPerson = person;
    this.newJob = { companyName: '', position: '', startDate: null, endDate: null };
    this.addJobDialog = true;
  }

  addJob() {
    if (this.newJob.companyName && this.newJob.position && this.newJob.startDate) {
      const jobData = {
        companyName: this.newJob.companyName,
        position: this.newJob.position,
        startDate: this.datePipe.transform(this.newJob.startDate, 'yyyy-MM-dd')!,
        endDate: this.newJob.endDate ? this.datePipe.transform(this.newJob.endDate, 'yyyy-MM-dd') : null,
      };

      this.service.addJobToPerson(this.selectedPerson.id, jobData).subscribe(
        () => {
          this.addJobDialog = false;
          this.service.load().subscribe();
        }
      );
    }
  }

  getByCompany() {
    if (this.company.name) {
      this.service.getByCompany(this.company.name).subscribe(
        (result) => {
          this.getByCompanyResult = result;
        }
      )
    }
  }
}