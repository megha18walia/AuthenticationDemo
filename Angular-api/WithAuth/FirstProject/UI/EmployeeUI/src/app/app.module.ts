import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { environment } from "src/environments/environment";
import { MsalModule, MsalInterceptor } from "@azure/msal-angular";
import {
  HttpClientModule,
  HttpClient,
  HTTP_INTERCEPTORS,
} from "@angular/common/http";
import { MsalUserService } from "./msaluser.service";

export const protectedResourceMap: any = [
  [environment.baseUrl, environment.scopeUri],
];

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    MsalModule.forRoot({
      clientID: environment.uiClienId,
      authority: "https://login.microsoftonline.com/" + environment.tenantId,
      protectedResourceMap: protectedResourceMap,
      redirectUri: environment.redirectUrl,
    }),
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
  ],
  providers: [
    HttpClient,
    MsalUserService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: MsalInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
