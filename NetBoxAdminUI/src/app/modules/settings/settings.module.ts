import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { ServiceManageComponent } from './service-manage/service-manage.component';
import { SettingsRoutingModule } from './settings.routes';
import { ServiceCreateComponent } from './service-manage/service-create/service-create.component';
import { ClientTypeComponent } from './client-type/client-type.component';
import { JobTypesComponent } from './job-types/job-types.component';
import { DesignReceivedMediumComponent } from './design-received-medium/design-received-medium.component';
import { AdvertisementMaterialComponent } from './advertisement-material/advertisement-material.component';



@NgModule({
  declarations: [
    ServiceManageComponent,
    ServiceCreateComponent,
    ClientTypeComponent,
    JobTypesComponent,
    DesignReceivedMediumComponent,
    AdvertisementMaterialComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    SettingsRoutingModule
  ]
})
export class SettingsModule { }
