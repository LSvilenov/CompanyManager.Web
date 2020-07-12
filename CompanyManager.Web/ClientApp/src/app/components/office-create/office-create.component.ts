import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OfficeService } from '../../services/office.service';
import { OfficeCreateModel } from '../../models/office-create.model';

@Component({
  selector: 'app-office-create',
  templateUrl: './office-create.component.html'
})

export class OfficeCreateComponent implements OnInit {
  constructor(
    private router: Router,
    private officeService: OfficeService) { }

  public office: OfficeCreateModel;

  ngOnInit(): void {
    this.office = {
      cityId: null,
      companyId: null,
      isHeadQuarters: false,
      street: '',
      streetNumber: null
    }
  }

  createOffice(office: OfficeCreateModel) {
    this.officeService.createOffice(office).subscribe(result => {
      this.router.navigate(['/office-details', result.id]);
    }, error => console.error(error));
  }
}
