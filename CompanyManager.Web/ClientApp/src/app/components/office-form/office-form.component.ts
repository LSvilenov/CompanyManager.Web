import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { CompanyCreateModel } from '../../models/company-create.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { OfficeCreateModel } from '../../models/office-create.model';
import { CompanyService } from '../../services/company.service';
import { NomenclatureItemModel } from '../../models/nomenclature-item.model';
import { GeoService } from '../../services/geo.service';

@Component({
  selector: 'app-office-form',
  templateUrl: './office-form.component.html'
})
export class OfficeFormFormComponent implements OnInit {
  @Input() office: OfficeCreateModel;

  @Output() onSave: EventEmitter<CompanyCreateModel> = new EventEmitter();

  public officeForm: FormGroup;

  public companies: NomenclatureItemModel[];

  public cities: NomenclatureItemModel[];

  constructor(private companyService: CompanyService, private geoService: GeoService) { }

  ngOnInit(): void {
    this.companies = [];
    this.cities = [];

    this.officeForm = new FormGroup({
      cityId: new FormControl(),
      street: new FormControl(),
      streetNumber: new FormControl('', [
        Validators.pattern("^[0-9]*$")
      ]),
      companyId: new FormControl(),
      isHeadQuarters: new FormControl()
    });

    this.officeForm.reset(this.office);

    this.companyService.getComanySelectList().subscribe(result => {
      this.companies = result;
    }, error => console.error(error));

    this.geoService.getCities().subscribe(result => {
      this.cities = result;
    }, error => console.error(error));
  }

  onSubmit() {
    if (this.officeForm.valid) {
      this.onSave.emit(this.officeForm.value);
    }
  }

  get streetNumber() { return this.officeForm.get('streetNumber'); }
}
