import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { EmployeeCreateModel } from '../../models/employee-create.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { NomenclatureItemModel } from '../../models/nomenclature-item.model';
import { OfficeService } from '../../services/office.service';
import { CompanyService } from '../../services/company.service';

@Component({
  selector: 'app-employee-form',
  templateUrl: './employee-form.component.html'
})
export class EmployeeFormComponent implements OnInit {
  @Input() employee: EmployeeCreateModel;

  @Output() onSave: EventEmitter<EmployeeCreateModel> = new EventEmitter();

  public employeeForm: FormGroup;
  public offices: NomenclatureItemModel[];
  public companies: NomenclatureItemModel[];
  public dropdownSettingsSingle = {};
  public dropdownSettingsMulti = {};

  constructor(private officeService: OfficeService, private companyService: CompanyService) { }

  ngOnInit(): void {
    this.offices = [];
    this.companies = [];

    this.employeeForm = new FormGroup({
      firstName: new FormControl(),
      lastName: new FormControl(),
      startingDate: new FormControl(),
      salary: new FormControl('', [
        Validators.pattern("^[-+]?[0-9]*\.?[0-9]+")
      ]),
      vacationDays: new FormControl('', [
        Validators.pattern("^[0-9]*$")
      ]),
      experienceLevel: new FormControl(),
      company: new FormControl(),
      officeIds: new FormControl()
    });

    this.employeeForm.reset(this.employee);

    this.companyService.getComanySelectList().subscribe(result => {
      this.companies = result;
    }, error => console.error(error));

    this.dropdownSettingsSingle = {
      singleSelection: true,
      idField: 'id',
      textField: 'name',
      itemsShowLimit: 5,
      allowSearchFilter: true,
      closeDropDownOnSelection: true
    };

    this.dropdownSettingsMulti = {
      singleSelection: false,
      idField: 'id',
      textField: 'name',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 10,
      allowSearchFilter: true,
    };
  }

  onSubmit() {
    if (this.employeeForm.valid) {
      this.onSave.emit(this.employeeForm.value);
    }
  }

  onItemSelect(item: NomenclatureItemModel) {
    this.populateOfficesForCompany(item.id);
  }

  onItemDeSelect(item: NomenclatureItemModel) {
    this.employeeForm.get('officeIds').setValue([]);
    this.offices = [];
  }

  populateOfficesForCompany(companyId: number) {
    this.officeService.getCompanyOfficesList(companyId).subscribe(result => {
      this.offices = result.map(c => <NomenclatureItemModel>{
        id: c.id,
        name: c.city + ', ' + c.street + ' ' + c.streetNumber
      })
    }, error => console.error(error));
  }

  get vacationDays() { return this.employeeForm.get('vacationDays'); }
  get salary() { return this.employeeForm.get('salary'); }

}
