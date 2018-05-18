
#import "BonjourClientImpl.h"

@implementation NetServiceBrowserDelegate


- (id)init
{
    self = [super init];
    services = [[NSMutableArray alloc] init];
    searching = NO;
	status = @"Initializing";
    return self;
}



- (void)dealloc
{
    [services release];
    [super dealloc];	
}



// Sent when browsing begins
- (void)netServiceBrowserWillSearch:(NSNetServiceBrowser *)browser
{
    searching = YES;
	status = @"Searching";
	
	[services removeAllObjects];
}



// Sent when browsing stops
- (void)netServiceBrowserDidStopSearch:(NSNetServiceBrowser *)browser
{
    searching = NO;
	status = @"Done";
}



// Sent if browsing fails
- (void)netServiceBrowser:(NSNetServiceBrowser *)browser
			 didNotSearch:(NSDictionary *)errorDict
{
    searching = NO;
	NSString * msg = @"Failed.";	
	status = [msg stringByAppendingString:[[errorDict objectForKey:NSNetServicesErrorCode] stringValue]];
}



// Sent when a service appears
- (void)netServiceBrowser:(NSNetServiceBrowser *)browser
		   didFindService:(NSNetService *)aNetService
			   moreComing:(BOOL)moreComing
{
    [services addObject:aNetService];
}



// Sent when a service disappears
- (void)netServiceBrowser:(NSNetServiceBrowser *)browser
		 didRemoveService:(NSNetService *)aNetService
			   moreComing:(BOOL)moreComing
{
    [services removeObject:aNetService];	
}


- (int)getCount
{
	return [services count];
}

- (NSNetService *)getService:(int)serviceNo
{
	return [services objectAtIndex:serviceNo];
}

- (NSString *)getStatus
{
	return status;
}

@end

static NetServiceBrowserDelegate* delegateObject = nil;
static NSNetServiceBrowser *serviceBrowser = nil;

// Converts C style string to NSString
NSString* CreateNSString (const char* string)
{
	if (string)
		return [NSString stringWithUTF8String: string];
	else
		return [NSString stringWithUTF8String: ""];
}

// Helper method to create C string copy
char* MakeStringCopy (const char* string)
{
	if (string == NULL)
		return NULL;
	
	char* res = (char*)malloc(strlen(string) + 1);
	strcpy(res, string);
	return res;
}

// When native code plugin is implemented in .mm / .cpp file, then functions
// should be surrounded with extern "C" block to conform C function naming rules
extern "C" {

	void _StartLookup (const char* service, const char* domain)
	{
		if (delegateObject == nil)
			delegateObject = [[NetServiceBrowserDelegate alloc] init];
		
		
		if (serviceBrowser == nil)
			serviceBrowser = [[NSNetServiceBrowser alloc] init];
		
		[serviceBrowser setDelegate:delegateObject];
		
		// Call "searchForServicesOfType" and pass NSStrings as parameters. By default mono
		// marshals all .Net strings as UTF-8 C style strings.
		[serviceBrowser searchForServicesOfType: CreateNSString(service) inDomain: CreateNSString(domain)];
	}
	
	const char* _GetLookupStatus ()
	{
		// By default mono string marshaler creates .Net string for returned UTF-8 C string 
		// and calls free for returned value, thus returned strings should be allocated on heap
		return MakeStringCopy([[delegateObject getStatus] UTF8String]);
	}
	
	int _GetServiceCount ()
	{
		return [delegateObject getCount];
	}
	
	const char* _GetServiceName (int serviceNumber)
	{
		// By default mono string marshaler creates .Net string for returned UTF-8 C string 
		// and calls free for returned value, thus returned strings should be allocated on heap
		return MakeStringCopy([[[delegateObject getService:serviceNumber] name] UTF8String]);
	}
	
	void _Stop()
	{
		[serviceBrowser stop];
	}
	
}

