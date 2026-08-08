import {ApplicationConfig, InjectionToken, LOCALE_ID, provideZoneChangeDetection} from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import {provideHttpClient, withJsonpSupport, withXhr} from '@angular/common/http';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import {environment} from '../environments/environment.development';
import {provideNativeDateAdapter} from '@angular/material/core';

export interface AppConfig {
  apiBaseUrl: string;
}

export const APP_CONFIG = new InjectionToken<AppConfig>('app.config');

export const DEFAULT_APP_CONFIG: AppConfig = {
  apiBaseUrl: environment.apiBaseUrl,
};

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(withXhr(), withJsonpSupport()),
    provideNativeDateAdapter(),
    { provide: APP_CONFIG, useValue: DEFAULT_APP_CONFIG},
    { provide: LOCALE_ID, useValue: 'fr-FR'}
  ]
};
