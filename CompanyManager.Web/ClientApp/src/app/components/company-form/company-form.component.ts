import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { CompanyListingViewModel } from '../../models/company-listing-view.model';
import { CompanyCreateModel } from '../../models/company-create.model';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-company-form',
  templateUrl: './company-form.component.html'
})
export class CompanyFormComponent implements OnInit {
  @Input() company: CompanyCreateModel;

  @Output() onSave: EventEmitter<CompanyCreateModel> = new EventEmitter();

  public companyForm: FormGroup;

  //public companies: CompanyListingViewModel;

  ngOnInit(): void {
    this.companyForm = new FormGroup({
      name: new FormControl(),
      creationDate: new FormControl()
    });

    this.companyForm.reset(this.company);
  }

  onSubmit() {
    this.onSave.emit(this.companyForm.value);
  }
}
