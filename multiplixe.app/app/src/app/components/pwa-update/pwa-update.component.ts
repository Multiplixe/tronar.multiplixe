import { Component, OnInit, OnDestroy } from '@angular/core';
import { SwUpdate } from '@angular/service-worker';
import { interval, Subscription } from 'rxjs';

@Component({
  selector: 'app-pwa-update',
  templateUrl: './pwa-update.component.html'
})
export class PwaUpdateComponent implements OnInit, OnDestroy {

  private updateSubscription: Subscription;
  public show: boolean = false;

  constructor(public updates: SwUpdate) {
  }

  ngOnDestroy(): void {
    this.updateSubscription.unsubscribe();
  }

  ngOnInit() {

    this.updateSubscription = this.updates.available.subscribe(event => {
      this.show = true;
    });

    if (this.updates.isEnabled) {
      this.updates.activateUpdate();
      interval(60 * 60 * 1000).subscribe(() => {
        this.updates.checkForUpdate().then(() => {
          window.location.reload();
        });
      });
    }
    // Important: on Safari (ios) Heroku doesn't auto redirect links to their https which allows the installation of the pwa like usual
    // but it deactivates the swUpdate. So make sure to open your pwa on safari like so: https://example.com then (install/add to home)
  }

  update(): void {
    this.updates.activateUpdate().then(() => {
      window.location.reload();
    });
  }
}
