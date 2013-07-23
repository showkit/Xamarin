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
	UIButton *_sendButton;
	UIScrollView *_shareContainer;
	UIButton *_shareGestureButton;
	UISwitch *_shareGestureOutlet;
	UITextField *_usernameTextField;
}

@property (nonatomic, retain) IBOutlet UIScrollView *callContainer;

@property (nonatomic, retain) IBOutlet UIView *mainVideoUIView;

@property (nonatomic, retain) IBOutlet UIView *prevVideoUIView;

@property (nonatomic, retain) IBOutlet UIButton *sendButton;

@property (nonatomic, retain) IBOutlet UIScrollView *shareContainer;

@property (nonatomic, retain) IBOutlet UIButton *shareGestureButton;

@property (nonatomic, retain) IBOutlet UISwitch *shareGestureOutlet;

@property (nonatomic, retain) IBOutlet UITextField *usernameTextField;

- (IBAction)makeCallAction:(id)sender;

- (IBAction)shareScreenAction:(id)sender;

- (IBAction)shareGestureAction:(id)sender;

- (IBAction)shareGestureSwitch:(id)sender;

- (IBAction)sendAction:(id)sender;

@end
