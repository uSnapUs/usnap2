//
//  KIFTestScenario+USScenarios.m
//  uSnap
//
//  Created by Owen Evans on 4/02/13.
//  Copyright (c) 2013 uSnap Holdings Ltd. All rights reserved.
//

#import "KIFTestScenario+USScenarios.h"
#import "KIFTestStep+GlobalSteps.h"


@implementation KIFTestScenario (USScenarios)
+(id)scenarioToInitialize{
     KIFTestScenario *scenario = [KIFTestScenario scenarioWithDescription:@"Test that the app starts up to the correct screen"];
     [scenario addStep:[KIFTestStep stepToReset]];
     return scenario;
}

@end
