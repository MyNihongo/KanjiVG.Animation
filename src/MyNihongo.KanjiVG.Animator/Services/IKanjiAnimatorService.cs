using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNihongo.KanjiVG.Animator.Services;

public interface IKanjiAnimatorService
{
	Task GenerateAsync(SvgParams svgParams);
}