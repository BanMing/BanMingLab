#import <Foundation/Foundation.h>



@interface NetServiceBrowserDelegate : NSObject
{
    // Keeps track of available services
    NSMutableArray *services;
	
	// Keeps track of search status
	NSString* status; 
    BOOL searching;
}



// NSNetServiceBrowser delegate methods for service browsing
- (void)netServiceBrowserWillSearch:(NSNetServiceBrowser *)browser;
- (void)netServiceBrowserDidStopSearch:(NSNetServiceBrowser *)browser;
- (void)netServiceBrowser:(NSNetServiceBrowser *)browser
			 didNotSearch:(NSDictionary *)errorDict;
- (void)netServiceBrowser:(NSNetServiceBrowser *)browser
		   didFindService:(NSNetService *)aNetService
			   moreComing:(BOOL)moreComing;
- (void)netServiceBrowser:(NSNetServiceBrowser *)browser
		 didRemoveService:(NSNetService *)aNetService
			   moreComing:(BOOL)moreComing;

// Other methods

- (int)getCount;
- (NSNetService *)getService:(int)serviceNo;
- (NSString *)getStatus;

@end

