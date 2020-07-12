import { Component, OnInit } from '@angular/core';
import { CompanyService } from '../../services/company.service';
import { CompanyListingViewModel } from '../../models/company-listing-view.model';

@Component({
  selector: 'app-company-list',
  templateUrl: './company-list.component.html'
})
export class CompanyListComponent implements OnInit {
  constructor(private companyService: CompanyService) { }

  public companies: CompanyListingViewModel;

  public filter: string;
  public searchInputValue: string;

  ngOnInit(): void {
    this.filter = '';
    this.searchInputValue = '';
    this.onCompanyPageChange(1);
  }

  onCompanyPageChange(page: number) {
    this.companyService.getCompanyList(this.filter, page, 5).subscribe(result => {
      this.companies = result;
    }, error => console.error(error));
  }

  public onSearch() {
    this.filter = this.searchInputValue;
    this.onCompanyPageChange(1);
  }
}
