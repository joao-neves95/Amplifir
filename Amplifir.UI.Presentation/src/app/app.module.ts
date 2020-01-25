/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ShoutCardComponent } from './components/shared/shout-card/shout-card.component';
import { ModalsComponent } from './components/modals/modals.component';
import { AuthModalComponent } from './components/modals/auth-modal/auth-modal.component';
import { NewShoutModalComponent } from './components/modals/new-shout-modal/new-shout-modal.component';
import { ShoutModalComponent } from './components/modals/shout-modal/shout-modal.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { PageHeaderComponent } from './components/page-header/page-header.component';
import { ExploreComponent } from './components/pages/explore/explore.component';
import { FeedComponent } from './components/pages/feed/feed.component';
import { ProfileComponent } from './components/pages/profile/profile.component';
import { SettingsComponent } from './components/pages/settings/settings.component';
import { ShoutsService, AuthService } from './services/apiClient.service';
import { NewShoutButtonComponent } from './components/shared/new-shout-button/new-shout-button.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    AuthModalComponent,
    NewShoutModalComponent,
    ShoutModalComponent,
    PageHeaderComponent,
    ProfileComponent,
    FeedComponent,
    SettingsComponent,
    ModalsComponent,
    ExploreComponent,
    ShoutCardComponent,
    NewShoutButtonComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [
    ShoutsService,
    AuthService
  ],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
