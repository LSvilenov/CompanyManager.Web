import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { OfficeService } from '../../services/office.service';
import { OfficeCreateModel } from '../../models/office-create.model';

@Component({
  selector: 'app-office-edit',
  templateUrl: './office-edit.component.html'
})
export class OfficeEditComponent implements OnInit {
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private officeService: OfficeService) { }

  officeId: number;

  public office: OfficeCreateModel;

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.officeId = params['id'];

      this.officeService.getOffice(this.officeId).subscribe(result => {
        this.office = {
          cityId: result.cityId,
          companyId: result.companyId,
          street: result.street,
          streetNumber: result.streetNumber,
          isHeadQuarters: result.isHeadQuarters
        }
      }, error => console.error(error));
    });
  }

  editOffice(office: OfficeCreateModel) {
    this.officeService.editOffice(this.officeId, office).subscribe(() => {
      this.router.navigate(['/office-details', this.officeId]);
    }, error => console.error(error))
  }
}

