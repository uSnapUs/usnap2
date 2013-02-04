//
//  KIFTestStep+GlobalSteps.m
//  uSnap
//
//  Created by Owen Evans on 4/02/13.
//  Copyright (c) 2013 uSnap Holdings Ltd. All rights reserved.
//

#import "KIFTestStep+GlobalSteps.h"

@implementation KIFTestStep (GlobalSteps)

+ (id)stepToReset;
{
    return [KIFTestStep stepWithDescription:@"Reset the application state." executionBlock:^(KIFTestStep *step, NSError **error) {
        BOOL successfulReset = YES;
        
        // Do the actual reset for your app. Set successfulReset = NO if it fails.
        
        KIFTestCondition(successfulReset, error, @"Failed to reset the application.");
        
        return KIFTestStepResultSuccess;
    }];
}

@end
