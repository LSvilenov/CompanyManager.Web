import { Component, OnInit } from '@angular/core';
import { OfficeService } from '../../services/office.service';
import { OfficeListingViewModel } from '../../models/office-listing-view.model';

@Component({
  selector: 'app-office-list',
  templateUrl: './office-list.component.html'
})

export class OfficeListComponent implements OnInit {
  constructor(private officeService: OfficeService) { }

  public offices: OfficeListingViewModel;

  public filter: string;
  public searchInputValue: string;

  ngOnInit(): void {
    this.filter = '';
    this.searchInputValue = '';
    this.onOfficePageChange(1);
  }

  onOfficePageChange(page: number) {
    this.officeService.getOfficesList(this.filter, page, 5).subscribe(result => {
      this.offices = result;
    }, error => console.error(error));
  }

  public onSearch() {
    this.filter = this.searchInputValue;
    this.onOfficePageChange(1);
  }
}
