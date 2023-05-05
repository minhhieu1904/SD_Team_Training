import { ModalService } from './_core/services/common/modal.service';
import { DashboardComponent } from './views/dashboard/dashboard.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { LocationStrategy, HashLocationStrategy } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';

import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';

import { IconModule, IconSetModule, IconSetService } from '@coreui/icons-angular';

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true
};

import { AppComponent } from './app.component';
import { GlobalHttpInterceptor } from '../app/_core/utilities/global-http-interceptor';
// Import containers
import { DefaultLayoutComponent } from './containers';

const APP_CONTAINERS = [
  DefaultLayoutComponent
];

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
import { NgxBreadcrumbModule } from "ngx-dynamic-breadcrumb";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { HeaderContainerComponent } from './containers/header-container/header-container.component';
import { FooterContainerComponent } from './containers/footer-container/footer-container.component';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { NgxPrintModule} from 'ngx-print';
import { LoginComponent } from './views/login/login.component';
import { AuthGuard } from '@guards/auth.guard';
import { AppGuard } from '@guards/app.guard';
import { ModalModule } from 'ngx-bootstrap/modal';
import { LocalStorageConstants } from '@constants/localStorge.constants';
import { JwtModule } from '@auth0/angular-jwt';
import { environment } from '@env/environment';
import { NgSelectModule } from '@ng-select/ng-select';
import { RouterModule } from '@angular/router';
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
    ModalModule.forRoot(),
    SnotifyModule,
    NgxSpinnerModule,
    NgxPrintModule,
    NgSelectModule,
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
    DefaultLayoutComponent,
    LoginComponent,
    DashboardComponent,
  ...APP_CONTAINERS,
    HeaderContainerComponent,
    FooterContainerComponent

  ],
  providers: [
    AppGuard,
    AuthGuard,
    {
      provide: LocationStrategy,
      useClass: HashLocationStrategy,
    },
    ModalService,
    { provide: 'SnotifyToastConfig', useValue: ToastDefaults },
    { provide: HTTP_INTERCEPTORS, useClass: GlobalHttpInterceptor, multi: true },
    SnotifyService,
    IconSetService,
  ],
  bootstrap: [ AppComponent ]
})
export class AppModule { }

