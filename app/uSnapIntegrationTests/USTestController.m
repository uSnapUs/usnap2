//
//  USTestController.m
//  uSnap
//
//  Created by Owen Evans on 4/02/13.
//  Copyright (c) 2013 uSnap Holdings Ltd. All rights reserved.
//

#import "USTestController.h"
#import "KIFTestScenario+USScenarios.h"

@implementation USTestController

-(void)initializeScenarios
{
    [self addScenario:[KIFTestScenario scenarioToInitialize]];
}

@end
