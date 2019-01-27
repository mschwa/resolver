/**
 *
 * Copyright (C) 2018 Integrated Financial Services
 *
 * Purpose: Items for Left Vertical Navigation, which includes expansion items and badges.
 *
 * Where Referenced:
 *   -
 *
 * Change History:
 *
 * Developer                     Date        	Flag		Description
 * -------------------------------------------------------------------------------------------
 * Marc Dysart                   11/28/2018		638		    Display Left Vertical Navigation Bar
 * 
 *
 */
import { FuseNavigation } from '@fuse/types';

export const navigation: FuseNavigation[] = [
    {
        id       : 'mycases',
        title    : 'My Cases',
        translate: 'NAV.MYCASES.TITLE',
        type     : 'collapsable',
        icon     : 'home',
        url      : '/mycases',
        
        children : [
            {
                id   : 'active',
                title: 'Active (10)',
                type : 'item',
                url  : '/mycases/active',
                badge    : {
                    title    : '3',
                    translate: 'NAV.MYCASES.BADGE',
                    bg       : '#F44336',
                    fg       : '#FFFFFF'
                } 
            },
            {
                id   : 'resolved',
                title: 'Resolved (12)',
                type : 'item',
                url  : '/mycases/resolved'
    
            },
            {
                id   : 'closed',
                title: 'Closed (13)',
                type : 'item',
                url  : '/mycases/closed'
            
            },
            {
                id   : 'recent',
                title: 'Recent (16)',
                type : 'item',
                url  : '/mycases/recent'
            },
            {
                id   : 'allcases',
                title: 'All Cases (71)',
                type : 'item',
                url  : '/mycases/allcases'
            }
        ]
    },
    {
        id       : 'easyestimate',
        title    : 'Easy Estimate',
        translate: 'NAV.EASYESTIMATE.TITLE',
        type     : 'item',
        icon     : 'wb_incandescent',
        url      : '/easyestimate'
    },
    {
        id       : 'refercase',
        title    : 'Refer a Case',
        translate: 'NAV.REFERCASE.TITLE',
        type     : 'item',
        icon     : 'add_circle',
        url      : '/refercase'
    },
    {
        id       : 'questions',
        title    : 'Questions',
        translate: 'NAV.QUESTIONS.TITLE',
        type     : 'item',
        icon     : 'live_help',
        url      : '/questions'
    },
    {
        id       : 'settings',
        title    : 'Settings',
        translate: 'NAV.SETTINGS.TITLE',
        type     : 'item',
        icon     : 'settings',
        url      : '/settings'
    },
];