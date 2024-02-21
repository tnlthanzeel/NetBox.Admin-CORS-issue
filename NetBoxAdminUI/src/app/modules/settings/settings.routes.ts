import { RouterModule, Routes } from "@angular/router";
import { ServiceManageComponent } from "./service-manage/service-manage.component";
import { NgModule } from "@angular/core";
import { ClientTypeComponent } from "./client-type/client-type.component";
import { JobTypesComponent } from "./job-types/job-types.component";
import { DesignReceivedMediumComponent } from "./design-received-medium/design-received-medium.component";
import { AdvertisementMaterialComponent } from "./advertisement-material/advertisement-material.component";

const routes: Routes = [
    {
        path: 'service',
        component: ServiceManageComponent,
    },
    {
        path: 'client-type',
        component: ClientTypeComponent,
    },
    {
        path: 'job-type',
        component: JobTypesComponent,
    },
    {
        path: 'design-received-medium',
        component : DesignReceivedMediumComponent
    },
    {
        path:'advertising-material',
        component : AdvertisementMaterialComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class SettingsRoutingModule { }
