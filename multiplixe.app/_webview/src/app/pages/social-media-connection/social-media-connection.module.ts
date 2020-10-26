import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { SocialMediaConnectionPageRoutingModule } from './social-media-connection-routing.module';

import { SocialMediaConnectionPage } from './social-media-connection.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    SocialMediaConnectionPageRoutingModule
  ],
  declarations: [SocialMediaConnectionPage]
})
export class SocialMediaConnectionPageModule {}
