import { StringEntry } from '../entries/string.entry';

export class PasswordResetEntries {
    public password: StringEntry = StringEntry.Create();
    public mode: string = '';
    public oobCode: string = '';
    public apiKey: string = '';

    static Create(): PasswordResetEntries {
        return new PasswordResetEntries();
    }

    resetAll() {
        this.password.reset();
    }

}