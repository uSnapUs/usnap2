//
//  DetailViewController.h
//  uSnap
//
//  Created by Owen Evans on 27/01/13.
//  Copyright (c) 2013 uSnap Holdings Ltd. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface DetailViewController : UIViewController

@property (strong, nonatomic) id detailItem;

@property (weak, nonatomic) IBOutlet UILabel *detailDescriptionLabel;
@end
