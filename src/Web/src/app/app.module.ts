import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { routing } from './app-routing.module';

import { AccordionModule } from 'primeng/accordion';

// PrimeNg
import {TableModule} from 'primeng/table';
import {DialogModule} from 'primeng/dialog';
import {ButtonModule} from 'primeng/button';
import {DropdownModule} from 'primeng/dropdown';

import { JwtInterceptor, ErrorInterceptor } from './_helpers';
import { HomeComponent } from './home';
import { BatchesComponent } from './batches';
import { StocksComponent } from './stocks';
import { AdminComponent } from './admin';
import { LoginComponent } from './login';

@NgModule({
  imports: [
    TableModule,
    DialogModule,
    ButtonModule,
    DropdownModule,
    AccordionModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserModule,
    BrowserAnimationsModule,
    routing,
    HttpClientModule
  ],
  declarations: [
    AppComponent,
    HomeComponent,
    BatchesComponent,
    StocksComponent,
    AdminComponent,
    LoginComponent
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
