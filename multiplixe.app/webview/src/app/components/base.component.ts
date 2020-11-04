import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { AppInjector } from '../app-injector';
import { EventEmitterService } from '../services/event-emitter.service';

@Component({
    selector: 'app-base-component',
    template: ''
})
export class BaseComponent implements OnDestroy {

    private subscriptions: Subscription = new Subscription();
    protected eventEmitter: EventEmitterService;

    constructor() {
        this.eventEmitter = AppInjector.get(EventEmitterService)
    }

    ngOnDestroy(): void {
        this.subscriptions.unsubscribe();
    }

    addSubscription(s: Subscription) {
        this.subscriptions.add(s);
    }
}