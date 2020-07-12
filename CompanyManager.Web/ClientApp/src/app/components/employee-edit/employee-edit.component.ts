import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeService } from '../../services/employee.service';
import { EmployeeCreateModel } from '../../models/employee-create.model';
import { OfficeService } from '../../services/office.service';
import { NomenclatureItemModel } from '../../models/nomenclature-item.model';

@Component({
  selector: 'app-employee-edit',
  templateUrl: './employee-edit.component.html'
})
export class EmployeeEditComponent implements OnInit {
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private employeeService: EmployeeService, private officeService: OfficeService) { }

  employeeId: number;

  public employee: EmployeeCreateModel;

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.employeeId = params['id'];
      let officesNomenclature: NomenclatureItemModel[];

      this.officeService.getEmployeeOfficesList(this.employeeId).subscribe(result => {

        officesNomenclature = result.map(o => <NomenclatureItemModel>{
          id: o.id,
          name: o.city + ', ' + o.street + ' ' + o.streetNumber
        })
      }, error => console.error(error));

      // Dirty hach but this is my last module and I don't have time to fix :(
      const experienceLevels = ['Junior', 'Mid', 'Senior']
      this.employeeService.getEmployee(this.employeeId).subscribe(result => {
        this.employee = {
          firstName: result.firstName,
          lastName: result.lastName,
          salary: result.salary,
          experienceLevel: experienceLevels.indexOf(result.experienceLevel),
          startingDate: result.startingDate,
          vacationDays: result.vacationDays,
          officeIds: [],
          offices: officesNomenclature
        }
      }, error => console.error(error));
    });
  }

  editEmployee(employee: EmployeeCreateModel) {
    this.employeeService.editEmployee(this.employeeId, employee).subscribe(() => {
      this.router.navigate(['/employee-details', this.employeeId]);
    }, error => console.error(error))
  }
}

