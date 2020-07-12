import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CompanyService } from './services/company.service';
import { CompanyListComponent } from './components/company-list/company-list.component';
import { CompanyDetailsComponent } from './components/company-details/company-details.component';
import { CompanyCreateComponent } from './components/company-create/company-create.component';
import { CompanyEditComponent } from './components/company-edit/company-edit.component';
import { CompanyFormComponent } from './components/company-form/company-form.component';
import { OfficeService } from './services/office.service';
import { OfficeListComponent } from './components/office-list/office-list.component';
import { OfficeDetailsComponent } from './components/office-details/office-details.component';
import { OfficeCreateComponent } from './components/office-create/office-create.component';
import { OfficeFormFormComponent } from './components/office-form/office-form.component';
import { GeoService } from './services/geo.service';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { OfficeEditComponent } from './components/office-edit/office-edit.component';
import { EmployeeService } from './services/employee.service';
import { EmployeeListComponent } from './components/employee-list/employee-list.component';
import { EmployeeDetailsComponent } from './components/employee-details/employee-details.component';
import { EmployeeFormComponent } from './components/employee-form/employee-form.component';
import { EmployeeCreateComponent } from './components/employee-create/employee-create.component';
import { EmployeeEditComponent } from './components/employee-edit/employee-edit.component';
import { UploadComponent } from './components/upload/upload.component';
import { ImageService } from './services/image.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CompanyListComponent,
    CompanyDetailsComponent,
    CompanyCreateComponent,
    CompanyEditComponent,
    CompanyFormComponent,
    OfficeListComponent,
    OfficeDetailsComponent,
    OfficeCreateComponent,
    OfficeFormFormComponent,
    OfficeEditComponent,
    EmployeeListComponent,
    EmployeeDetailsComponent,
    EmployeeFormComponent,
    EmployeeCreateComponent,
    EmployeeEditComponent,
    UploadComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'companies', component: CompanyListComponent },
      { path: 'company-details/:id', component: CompanyDetailsComponent },
      { path: 'company-create', component: CompanyCreateComponent },
      { path: 'company-edit/:id', component: CompanyEditComponent },
      { path: 'offices', component: OfficeListComponent },
      { path: 'office-details/:id', component: OfficeDetailsComponent },
      { path: 'office-create', component: OfficeCreateComponent },
      { path: 'office-edit/:id', component: OfficeEditComponent },
      { path: 'employees', component: EmployeeListComponent },
      { path: 'employee-details/:id', component: EmployeeDetailsComponent },
      { path: 'employee-create', component: EmployeeCreateComponent },
      { path: 'employee-edit/:id', component: EmployeeEditComponent }
    ]),
    NgMultiSelectDropDownModule.forRoot()
  ],
  providers: [
    CompanyService,
    OfficeService,
    EmployeeService,
    GeoService,
    ImageService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
