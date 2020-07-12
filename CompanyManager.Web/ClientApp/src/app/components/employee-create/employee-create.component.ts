import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { EmployeeService } from '../../services/employee.service';
import { EmployeeCreateModel } from '../../models/employee-create.model';

@Component({
  selector: 'app-employee-create',
  templateUrl: './employee-create.component.html'
})

export class EmployeeCreateComponent implements OnInit {
  constructor(
    private router: Router,
    private employeeService: EmployeeService) { }

  public employee: EmployeeCreateModel;

  ngOnInit(): void {
    this.employee = {
      firstName: '',
      lastName: '',
      salary: null,
      experienceLevel: null,
      startingDate: null,
      vacationDays: 20,
      officeIds: [],
      offices: []
    }
  }

  createEmployee(employee: EmployeeCreateModel) {
    console.log(employee);
    const officeIds = employee.officeIds.map(function (officeNomenclature) {
      return officeNomenclature['id'];
    })

    employee.officeIds = officeIds;
    console.log(employee);

    this.employeeService.createEmployee(employee).subscribe(result => {
      this.router.navigate(['/employee-details', result.id]);
    }, error => console.error(error));
  }
}
