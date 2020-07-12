import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../../services/employee.service';
import { EmployeeListingViewModel } from '../../models/employee-listing-view.model';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html'
})
export class EmployeeListComponent implements OnInit {
  constructor(private employeeService: EmployeeService) { }

  public employees: EmployeeListingViewModel;

  public filter: string;

  public searchInputValue: string;

  ngOnInit(): void {
    this.filter = '';
    this.searchInputValue = '';
    this.onEmployeePageChange(1);
  }
  onEmployeePageChange(page: number) {
    this.employeeService.getEmployeesList(this.filter, page, 5).subscribe(result => {
      this.employees = result;
    }, error => console.error(error));
  }

  public onSearch() {
    this.filter = this.searchInputValue;
    this.onEmployeePageChange(1);
  }
}
