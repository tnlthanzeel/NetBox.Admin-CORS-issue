import { ApplicationConfig, importProvidersFrom, isDevMode } from '@angular/core';
import { PreloadAllModules, provideRouter, withDebugTracing, withPreloading, withViewTransitions } from '@angular/router';

import { routes } from './app.routes';
import { provideServiceWorker } from '@angular/service-worker';
import { HttpClientModule } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes,
    withPreloading(PreloadAllModules),
    withDebugTracing(),
    withViewTransitions()
  ), 
  importProvidersFrom(HttpClientModule, ToastrModule.forRoot({
    timeOut: 3000,
    positionClass: 'toast-bottom-right',
    preventDuplicates: true,
  })),
   provideServiceWorker('ngsw-worker.js', {
    enabled: !isDevMode(),
    registrationStrategy: 'registerWhenStable:30000'
  })]
};
