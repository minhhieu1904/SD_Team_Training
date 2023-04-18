import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { LocationStrategy, HashLocationStrategy } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { PERFECT_SCROLLBAR_CONFIG } from 'ngx-perfect-scrollbar';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';

import {
  IconModule,
  IconSetModule,
  IconSetService,
} from '@coreui/icons-angular';

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true,
};

import { AppComponent } from './app.component';
import { GlobalHttpInterceptor } from '../app/_core/utilities/global-http-interceptor';
// Import containers
import { DefaultLayoutComponent } from './containers';

const APP_CONTAINERS = [DefaultLayoutComponent];

import {
  AppAsideModule,
  AppBreadcrumbModule,
  AppHeaderModule,
  AppFooterModule,
  AppSidebarModule,
} from '@coreui/angular';

// Import routing module
import { AppRoutingModule } from './app.routing';

// Import 3rd party components
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { ChartsModule } from 'ng2-charts';
import { SnotifyModule, SnotifyService, ToastDefaults } from 'ng-snotify';
import { NgxSpinnerModule } from 'ngx-spinner';
import { NgxBreadcrumbModule } from 'ngx-dynamic-breadcrumb';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {
  HttpClient,
  HttpClientModule,
  HTTP_INTERCEPTORS,
} from '@angular/common/http';
import { HeaderContainerComponent } from './containers/header-container/header-container.component';
import { FooterContainerComponent } from './containers/footer-container/footer-container.component';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { NgxPrintModule } from 'ngx-print';
import { RouterModule } from '@angular/router';
import { DashboardComponent } from './views/dashboard/dashboard.component';
import { LoginComponent } from './views/login/login.component';
import { AuthGuard } from '@guards/auth.guard';
import { LocalStorageConstants } from '@constants/local-storage.constants';
import { JwtModule } from '@auth0/angular-jwt';
import { environment } from '@env/environment';
import { AppGuard } from '@guards/app.guard';
export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}
export function tokenGetter() {
  return localStorage.getItem(LocalStorageConstants.TOKEN);
}
@NgModule({
  imports: [
    HttpClientModule,
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    AppAsideModule,
    AppBreadcrumbModule.forRoot(),
    AppFooterModule,
    AppHeaderModule,
    AppSidebarModule,
    PerfectScrollbarModule,
    BsDropdownModule.forRoot(),
    TabsModule.forRoot(),
    ChartsModule,
    IconModule,
    IconSetModule.forRoot(),
    SnotifyModule,
    NgxSpinnerModule,
    NgxPrintModule,
    RouterModule,
    NgxBreadcrumbModule.forRoot(),
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient],
      },
    }),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: environment.allowedDomains,
        disallowedRoutes: environment.disallowedRoutes,
      },
    }),
  ],
  declarations: [
    AppComponent,
    ...APP_CONTAINERS,
    HeaderContainerComponent,
    LoginComponent,
    DashboardComponent,
    FooterContainerComponent,
    DefaultLayoutComponent,
  ],
  providers: [
    AuthGuard,
    AppGuard,
    {
      provide: LocationStrategy,
      useClass: HashLocationStrategy,
    },
    { provide: 'SnotifyToastConfig', useValue: ToastDefaults },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: GlobalHttpInterceptor,
      multi: true,
    },
    SnotifyService,
    IconSetService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
