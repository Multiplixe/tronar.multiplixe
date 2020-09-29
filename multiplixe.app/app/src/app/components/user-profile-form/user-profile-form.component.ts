import { Component, OnInit, Input } from '@angular/core';
import UserProfileEntry from 'src/app/dtos/user-profile.entries';
import { BaseComponent } from '../base.component';

@Component({
  selector: 'app-user-profile-form',
  templateUrl: './user-profile-form.component.html'
})
export class UserProfileFormComponent extends BaseComponent implements OnInit {

  @Input()
  public user: UserProfileEntry;

  @Input()
  public isRegister: boolean = false;

  constructor() {
    super();
  }

  ngOnInit() { }

}
