import { NgModule } from '@angular/core';
import { LocalStorageService } from '../services/local-storage.service';
import { HttpService } from '../services/http.service';
import { AuthGuard } from '../guards/auth.guard';
import { LoaderService } from '../services/loader.service';
import { FacebookModule, FacebookService } from 'ngx-facebook';
import { ToastService } from '../services/toast.service';
import { ErrorLogService } from '../services/error-log.service';
import { RouterRedirectService } from '../services/router-redirect.service';

import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { Factory as ActivityFactory } from '../services/activities/factory';
import { Factory as ActivityCommmonFactory } from '../services/activities-common/factory';
import { EventEmitterService } from '../services/event-emitter.service';
import { FirebaseListener } from '../services/activities/firebase-listener';
import { FirebaseListener as FirebaseListenerCommon } from '../services/activities-common/firebase-listener';
import { PushNotificationService } from '../services/push-notification.service';
import { FirebaseQuery } from '../services/firebase-query.service';
import { SetupService } from '../services/setup.service';
import { ngfModule } from "angular-file";
import { ImageCropperModule } from 'ngx-image-cropper';
import { CacheService } from '../services/cache.service';

@NgModule({
  imports: [FormsModule, CommonModule, IonicModule, FacebookModule, ngfModule, ImageCropperModule],
  exports: [FormsModule, CommonModule, IonicModule, FacebookModule, ngfModule, ImageCropperModule],
  providers: [
    LocalStorageService,
    HttpService,
    AuthGuard,
    LoaderService,
    FacebookService,
    ErrorLogService,
    ToastService,
    ActivityFactory,
    ActivityCommmonFactory,
    RouterRedirectService,
    EventEmitterService,
    FirebaseListener,
    FirebaseListenerCommon,
    PushNotificationService,
    CacheService,
    FirebaseQuery,
    SetupService,
    ngfModule, 
    ImageCropperModule]
})
export class SharedModule { }
