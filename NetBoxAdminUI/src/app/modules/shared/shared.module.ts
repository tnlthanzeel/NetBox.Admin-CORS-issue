import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { MaterialModule } from './material.module';
import { BlockUiComponent } from './components/block-ui/block-ui.component';
import { AuthorizationDirective } from './directives/authorization.directive';
import { NoRecordsComponent } from './components/no-records/no-records.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';



@NgModule({
  declarations: [
    BlockUiComponent,
    NoRecordsComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule,
    MaterialModule,
    NgbModule,
    AuthorizationDirective
  ],
  exports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule,
    MaterialModule,
    BlockUiComponent,
    AuthorizationDirective,
    NoRecordsComponent,
    NgbModule
  ]
})
export class SharedModule { }
