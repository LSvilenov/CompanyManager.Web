import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EmployeeService } from '../../services/employee.service';
import { EmployeeDetailsModel } from '../../models/employee-details.model';

@Component({
  selector: 'app-employee-details',
  templateUrl: './employee-details.component.html'
})

export class EmployeeDetailsComponent implements OnInit {
  constructor(
    private route: ActivatedRoute,
    private employeeService: EmployeeService) { }

  public employee: EmployeeDetailsModel;

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const id = params['id'];

      this.employeeService.getEmployee(id).subscribe(result => {
        this.employee = result;
      }, error => console.error(error));
    })
  }
}
