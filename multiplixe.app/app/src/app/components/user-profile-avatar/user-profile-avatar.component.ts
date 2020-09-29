import { Component, OnInit, Input } from '@angular/core';
import { AngularFireStorage } from '@angular/fire/storage';
import { AppInjector } from 'src/app/app-injector';
import { BaseComponent } from '../base.component';
import { ActivitiesEnum } from 'src/app/enums/activities.enum';
import { FirebaseQuery } from 'src/app/services/firebase-query.service';
import { DisplayControl } from 'src/app/dtos/display-control';

@Component({
  selector: 'app-user-profile-avatar',
  templateUrl: './user-profile-avatar.component.html'
})
export class UserProfileAvatarComponent extends BaseComponent implements OnInit {

  @Input()
  public size: number = 150;

  @Input()
  public showSkin: boolean = false;


  public defaultImage: string = '/assets/img/avatar/default.jpg';
  public imageUrl: string = '';
  private loopImageChange: number = 0;

  private angularFireStorage: AngularFireStorage;
  private firebaseQuery: FirebaseQuery;

  constructor() {
    super();
    this.angularFireStorage = AppInjector.get(AngularFireStorage);
    this.firebaseQuery = AppInjector.get(FirebaseQuery);
    this.defaultAvatar();

    this.addSubscription(
      this.eventEmitter
        .get(ActivitiesEnum.avatar)
        .subscribe(a => {
          this.refresh(a, 0);
        }));
  }

  ngOnInit() {
    this.load();
  }

  async load() {
    var snapshot = await this.firebaseQuery.avatar();
    this.refresh(snapshot.val(), 0);
  }

  defaultAvatar() {
    this.imageUrl = this.defaultImage;
  }

  refresh(avatar: any, _try: number) {

    if (avatar) {

      setTimeout(() => {

        this.angularFireStorage
          .ref("user-profile")
          .child("avatar")
          .child(avatar.image)
          .getDownloadURL()
          .toPromise()
          .then((i) => {
            this.setImage(i, avatar);
          })
          .catch(() => {
            if (_try < 5) {
              this.refresh(avatar, ++_try);
            }
            else {
              this.canShowContent();
            }
          });

      }, 1000);

    }
    else {
      this.canShowContent();
    }
  }

  private setImage(url: string, avatar: any) {
    if (url) {
      this.loopImageChange = 0;
      this.imageChange(url, avatar.timestamp);
    }
  }

  private imageChange(url: string, timestamp: string) {

    setTimeout(() => {

      timestamp += timestamp;

      this.imageUrl = url + '&v=' + timestamp

      if (this.loopImageChange < 5) {
        this.loopImageChange++;
        this.imageChange(url, timestamp);
      }
      else {
        this.canShowContent();
      }
    }, 500);
  }
}
