import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { TwitchAuthCallbackPageRoutingModule } from './twitch-auth-callback-routing.module';

import { TwitchAuthCallbackPage } from './twitch-auth-callback.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    TwitchAuthCallbackPageRoutingModule
  ],
  declarations: [TwitchAuthCallbackPage]
})
export class TwitchAuthCallbackPageModule {}
