// WARNING
// This file has been generated automatically by Xamarin Studio to
// mirror C# types. Changes in this file made by drag-connecting
// from the UI designer will be synchronized back to C#, but
// more complex manual changes may not transfer correctly.


#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>


@interface LoginViewController : UIViewController {
	UITextField *_passwordTextField;
	UITextField *_usernameTextField;
}

@property (nonatomic, retain) IBOutlet UITextField *passwordTextField;

@property (nonatomic, retain) IBOutlet UITextField *usernameTextField;

- (IBAction)loginAction:(id)sender;

- (IBAction)parseLoginAction:(id)sender;

@end
