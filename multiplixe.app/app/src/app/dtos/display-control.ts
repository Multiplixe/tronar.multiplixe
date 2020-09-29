export class DisplayControl {

    public step: any = undefined;
    private timeout: any;

    set(s: any) {
        this.step = s;
    }

    is(s: any) {
        return s == this.step;
    }

    isAndReset(s: any, seconds: number) {

        let is = s == this.step;

        clearTimeout(this.timeout);

        if (is) {
            this.timeout = setTimeout(() => {
                this.reset();
            }, seconds * 1000);
        }

        return is;
    }

    t() {
        return this.step;
    }

    has() {
        return this.step != undefined;
    }

    reset() {
        this.step = undefined;
    }

    any(s: string[]) {
        return s.find((f) => { return f == this.step }) != undefined;
    }

    static create(): DisplayControl {
        return new DisplayControl();
    }

}