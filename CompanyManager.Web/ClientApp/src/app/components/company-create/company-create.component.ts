import { Component, OnInit } from '@angular/core';
import { CompanyService } from '../../services/company.service';
import { CompanyCreateModel } from '../../models/company-create.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-company-create',
  templateUrl: './company-create.component.html'
})
export class CompanyCreateComponent implements OnInit {
  constructor(
    private router: Router,
    private companyService: CompanyService) {}

  public company: CompanyCreateModel;

  ngOnInit(): void {
    this.company = {
      name: '',
      creationDate: null
    }
  }

  createCompany(company: CompanyCreateModel) {
    this.companyService.createCompany(company).subscribe(result => {
      this.router.navigate(['/company-details', result.id]);
    }, error => console.error(error))
  }
}
