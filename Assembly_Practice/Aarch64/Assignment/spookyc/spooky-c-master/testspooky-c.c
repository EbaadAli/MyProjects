// SpookyHash: a 128-bit noncryptographic hash function
// By Bob Jenkins
//   Apr 27 2012: C port by Ziga Zupanec ziga.zupanec@gmail.com (agiz@github)
//

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <stdint.h>
#include <time.h>
#include <inttypes.h>

#include "spooky-c.h"

#define __STDC_FORMAT_MACROS
#define BILLION 1E9

static inline uint64_t rot64(uint64_t x, int k)
{
	return (x << k) | (x >> (64 - k));
}

struct random_vector
{
	uint64_t a;
	uint64_t b;
	uint64_t c;
	uint64_t d;
};

static inline uint64_t random_value(struct random_vector *m)
{
	uint64_t e = m->a - rot64(m->b, 23);
	m->a = m->b ^ rot64(m->c, 16);
	m->b = m->c + rot64(m->d, 11);
	m->c = m->d + e;
	m->d = e + m->a;

	return m->d;
}

static inline void random_init(struct random_vector *m, uint64_t seed)
{
	int i;
	m->a = 0xdeadbeef;
	m->b = m->c = m->d = seed;

	for (i=0; i<20; ++i)
	{
		(void)random_value(m);
	}
}


#define NUMBUF (1<<10)
#define BUFSIZE (1<<20)
void DoTimingBig(int seed)
{
	double t = 0.0F;
	struct timespec ts, tp;
	char *buf[NUMBUF];
	int i;
	uint64_t j;

	printf("\ntesting time to hash 2^^30 bytes ...\n");

	for (i=0; i<NUMBUF; ++i)
	{
		buf[i] = (char *)malloc(BUFSIZE);
		memset(buf[i], (char)seed, BUFSIZE);
	}

	uint64_t hash1 = seed;
	uint64_t hash2 = seed;

	clock_gettime(CLOCK_MONOTONIC, &ts);
	for (j=0; j<NUMBUF; ++j)
	{
		spooky_hash128(buf[j], BUFSIZE, &hash1, &hash2);
	}
	clock_gettime(CLOCK_MONOTONIC, &tp);
	t = (tp.tv_sec - ts.tv_sec) + (tp.tv_nsec - ts.tv_nsec) / BILLION;
	printf("SpookyHash::Hash128, uncached: time is %lf milliseconds\n", t);


	clock_gettime(CLOCK_MONOTONIC, &ts);
	for (j=0; j<NUMBUF*BUFSIZE/1024; ++j)
	{
		spooky_hash128(buf[0], 1024, &hash1, &hash2);
	}
	clock_gettime(CLOCK_MONOTONIC, &tp);
	t = (tp.tv_sec - ts.tv_sec) + (tp.tv_nsec - ts.tv_nsec) / BILLION;
	printf("SpookyHash::Hash128,   cached: time is %lf milliseconds\n", t);


	for (i=0; i<NUMBUF; ++i)
	{
		free(buf[i]);
		buf[i] = 0;
	}
}
#undef NUMBUF
#undef BUFSIZE


int main(int argc, const char **argv)
{
	(void) argv;

	DoTimingBig(argc);
	

	return 0;
}
