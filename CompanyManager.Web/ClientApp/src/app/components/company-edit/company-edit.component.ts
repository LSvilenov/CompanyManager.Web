import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CompanyService } from '../../services/company.service';
import { CompanyCreateModel } from '../../models/company-create.model';

@Component({
  selector: 'app-company-edit',
  templateUrl: './company-edit.component.html'
})
export class CompanyEditComponent implements OnInit {
    constructor(
      private route: ActivatedRoute,
      private router: Router,
      private companyService: CompanyService) {}

    companyId: number;

    public company: CompanyCreateModel;

    ngOnInit(): void {
			this.route.params.subscribe(params => {
				this.companyId = params['id'];

        this.companyService.getCompany(this.companyId).subscribe(result => {
          this.company = {
            name: result.name,
            creationDate: result.creationDate
          }
        }, error => console.error(error));
			});
    }

    editCompany(company: CompanyCreateModel) {
      this.companyService.editCompany(this.companyId, company).subscribe(() => {
        this.router.navigate(['/company-details', this.companyId]);
      }, error => console.error(error))
    }
}

