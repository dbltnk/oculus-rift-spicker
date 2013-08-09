using System;
using System.Collections;
using System.Collections.Generic;

public class Tuple<A,B>
{
	public A a;
	public B b;
	
	public Tuple(A a, B b)
	{
		this.a = a;
		this.b = b;
	}
}

public class Tuple<A,B,C>
{
	public A a;
	public B b;
	public C c;
	
	public Tuple(A a, B b, C c)
	{
		this.a = a;
		this.b = b;
		this.c = c;
	}
}

public class Tuple<A,B,C,D>
{
	public A a;
	public B b;
	public C c;
	public D d;
	
	public Tuple(A a, B b, C c, D d)
	{
		this.a = a;
		this.b = b;
		this.c = c;
		this.d = d;
	}
}
