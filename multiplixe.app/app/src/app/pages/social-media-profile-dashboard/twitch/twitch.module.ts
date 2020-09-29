import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { TwitchPageRoutingModule } from './twitch-routing.module';

import { TwitchPage } from './twitch.page';
import { SharedModule } from 'src/app/shared-module/shared.module';
import { ComponentModule } from 'src/app/components/component.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    TwitchPageRoutingModule,
    SharedModule,
    ComponentModule    
  ],
  declarations: [TwitchPage]
})
export class TwitchPageModule {}
