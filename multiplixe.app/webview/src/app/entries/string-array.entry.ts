import { BaseEntry } from './base.entry'

export class StringArrayEntry extends BaseEntry<string[]> {

    constructor(value: string[]) {
        super(value)
    }

}