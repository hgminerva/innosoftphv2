/// <reference path="jquery.d.ts" />

interface JQuery {
    jqote(data: any, tag?: string): Element;
    jqoteapp(template: string, data: any, tag?: string): JQuery;
    jqoteapp(template: JQuery, data: any, tag?: string): JQuery;
    jqoteapp(template: Element, data: any, tag?: string): JQuery;
    jqoteapp(template: Element[], data: any, tag?: string): JQuery;
    jqoteapp(template: Function, data: any, tag?: string): JQuery;
    jqoteapp(template: Function[], data: any, tag?: string): JQuery;
    jqotepre(template: string, data: any, tag?: string): JQuery;
    jqotepre(template: JQuery, data: any, tag?: string): JQuery;
    jqotepre(template: Element, data: any, tag?: string): JQuery;
    jqotepre(template: Element[], data: any, tag?: string): JQuery;
    jqotepre(template: Function, data: any, tag?: string): JQuery;
    jqotepre(template: Function[], data: any, tag?: string): JQuery;
    jqotesub(template: string, data: any, tag?: string): JQuery;
    jqotesub(template: JQuery, data: any, tag?: string): JQuery;
    jqotesub(template: Element, data: any, tag?: string): JQuery;
    jqotesub(template: Element[], data: any, tag?: string): JQuery;
    jqotesub(template: Function, data: any, tag?: string): JQuery;
    jqotesub(template: Function[], data: any, tag?: string): JQuery;

}

interface JQueryStatic {
    jqote(template: string, data: any, tag?: string): string;
    jqote(template: JQuery, data: any, tag?: string): string;
    jqote(template: Element, data: any, tag?: string): string;
    jqote(template: Element[], data: any, tag?: string): string;
    jqote(template: Function, data: any, tag?: string): string;
    jqotec(template: string, tag?: string): Function;
    jqotec(template: JQuery, tag?: string): Function;
    jqotec(template: Element, tag?: string): Function;
    jqotefn(template: string): Function;
    jqotefn(template: JQuery): Function;
    jqotefn(template: Element): Function;
    jqotetag(str: string): void;
}