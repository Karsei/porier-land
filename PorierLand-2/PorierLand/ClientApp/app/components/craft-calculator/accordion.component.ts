import { Component, EventEmitter, Input, Output } from '@angular/core';
import { trigger, state, style, animate, transition } from '@angular/animations';

import {
    Accordion,
    Item,
    ItemType
} from './craft-calculator.service';

@Component({
    selector: 'accordion',
    templateUrl: './accordion.component.html',
    styleUrls: ['./accordion.component.css'],
    animations: [
        trigger('appearTrigger', [
            state('in', style({
                transform: 'scale(1)'
            })),
            transition('void => *', [
                style({
                    transform: 'scale(0)'
                }),
                animate(200)
            ]),
            transition('* => void', [
                animate(200, style({
                    transform: 'scale(0)'
                }))
            ])
        ])
    ]
})
export class AccordionComponent {
    @Input()
    topAccordions: Accordion[];

    @Input()
    accordions: Accordion[];

    @Input()
    identifier: string;

    @Output()
    onCancelItem: EventEmitter<Item> = new EventEmitter<Item>();

    ItemType = ItemType;

    accordionTriggerStarted($event: any): void {
        $($event.element).tab();
    }

    toggleItem(item: Item): void {
        item.prepared = !item.prepared;
        if (item.parentAccordion) {
            this.refreshAccordion(item.parentAccordion);
        }
    }

    refreshAccordion(accordion: Accordion): void {
        if (!accordion) return;
        
        accordion.activeItemCount = 0;

        for (const item of accordion.items) {
            if (!item.prepared) accordion.activeItemCount++;
        }

        for (const child of accordion.childAccordions) {
            accordion.activeItemCount += child.activeItemCount;
        }

        accordion.prepared = accordion.activeItemCount === 0;

        if (accordion.childAccordions.length === 0 && accordion.items.length === 0) {
            let parentAccordionArray: Accordion[] = [];

            if (accordion.parentAccordion) {
                parentAccordionArray = accordion.parentAccordion.childAccordions;
            } else {
                parentAccordionArray = this.topAccordions;
            }

            parentAccordionArray.splice(parentAccordionArray.indexOf(accordion), 1);
        }

        if (accordion.parentAccordion) {
            this.refreshAccordion(accordion.parentAccordion);
        }
    }

    chainOnCancelItem(item: Item): void {
        this.onCancelItem.emit(item);
    }

    cancelItem(item: Item): void {
        if (item.parentAccordion) {
            item.parentAccordion.items.splice(item.parentAccordion.items.indexOf(item), 1);
            this.refreshAccordion(item.parentAccordion);
        }

        this.chainOnCancelItem(item);
    }

    removeItem(item: Item): void {
        if (item.parentAccordion) {
            item.parentAccordion.items.splice(item.parentAccordion.items.indexOf(item), 1);
            this.refreshAccordion(item.parentAccordion);
        }
    }
}

declare let $: {
    (selector: any): any;
}