import { BaseEntry } from './base.entry'

export class StringEntry extends BaseEntry<string>  {

    constructor(value: string) {
        super(value)
    }

    static Create(value?: string): StringEntry {
        return new StringEntry(value);
    }
}