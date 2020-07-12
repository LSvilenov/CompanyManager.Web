import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CompanyService } from '../../services/company.service';
import { CompanyDetailsModel } from '../../models/company-details.model';
import { OfficeListingServiceModel } from '../../models/office-listing-service.model';

@Component({
  selector: 'app-company-details',
  templateUrl: './company-details.component.html'
})
export class CompanyDetailsComponent implements OnInit {
  constructor(
    private route: ActivatedRoute,
    private companyService: CompanyService) { }

  public company: CompanyDetailsModel;
  public offices: OfficeListingServiceModel;

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const id = params['id'];

      this.companyService.getCompany(id).subscribe(result => {
        this.company = result;
      }, error => console.error(error));

      this.companyService.getOffices(id).subscribe(result => {
        this.offices = result;
      }, error => console.error(error));
    });
  }

}

