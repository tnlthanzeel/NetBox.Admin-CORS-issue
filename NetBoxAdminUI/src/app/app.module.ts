import { NgModule, isDevMode } from "@angular/core";
import { LayoutModule } from "./layout/layout.module";
import { SharedModule } from "./modules/shared/shared.module";
import { AuthModule } from "./modules/auth/auth.module";
import { ToastrModule } from "ngx-toastr";
import { AppComponent } from "./app.component";
import { AppRoutingModule } from "./app.routes";
import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ServiceWorkerModule } from "@angular/service-worker";
import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { TokenInterceptor } from "./core/interceptors/token.interceptor";
import { UserModule } from "./modules/user/user.module";


@NgModule({
  declarations: [AppComponent],
  bootstrap: [AppComponent],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    LayoutModule,
    AuthModule,
    UserModule,
    SharedModule,
    ServiceWorkerModule.register('ngsw-worker.js', {
      enabled: !isDevMode(),
      registrationStrategy: 'registerWhenStable:30000'
    }),
    ToastrModule.forRoot({
      timeOut: 3000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
    }),
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }
  ],
})
export class AppModule { }
