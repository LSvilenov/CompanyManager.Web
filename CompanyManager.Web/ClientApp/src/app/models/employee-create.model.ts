import { NomenclatureItemModel } from "./nomenclature-item.model";

export interface EmployeeCreateModel {
  firstName: string;
  lastName: string;
  startingDate: Date;
  salary: number;
  vacationDays: number;
  experienceLevel: number;
  officeIds: number[];
  offices: NomenclatureItemModel[];
}
