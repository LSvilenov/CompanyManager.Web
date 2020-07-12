import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OfficeService } from '../../services/office.service';
import { OfficeDetailsModel } from '../../models/office-details.model';
import { EmployeeListingServiceModel } from '../../models/employee-listing-service.model';

@Component({
  selector: 'app-office-details',
  templateUrl: './office-details.component.html'
})

export class OfficeDetailsComponent implements OnInit {
  constructor(
    private route: ActivatedRoute,
    private officeService: OfficeService) { }

  public office: OfficeDetailsModel;
  public employees: EmployeeListingServiceModel

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const id = params['id'];

      this.officeService.getOffice(id).subscribe(result => {
        this.office = result;
      }, error => console.error(error));

      this.officeService.getEmployees(id).subscribe(result => {
        this.employees = result;
      }, error => console.error(error));
    })
  }
}
