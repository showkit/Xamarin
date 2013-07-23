// WARNING
// This file has been generated automatically by Xamarin Studio to
// mirror C# types. Changes in this file made by drag-connecting
// from the UI designer will be synchronized back to C#, but
// more complex manual changes may not transfer correctly.


#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>


@interface DashboardViewController : UIViewController {
	UIScrollView *_callContainer;
	UIView *_mainVideoUIView;
	UIView *_prevVideoUIView;
	UITextField *_usernameTextField;
}

@property (nonatomic, retain) IBOutlet UIScrollView *callContainer;

@property (nonatomic, retain) IBOutlet UIView *mainVideoUIView;

@property (nonatomic, retain) IBOutlet UIView *prevVideoUIView;

@property (nonatomic, retain) IBOutlet UITextField *usernameTextField;

- (IBAction)makeCallAction:(id)sender;

@end
