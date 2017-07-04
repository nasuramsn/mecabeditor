package jp.co.nasuramsn.sitewarapper.app.controller;

import javax.validation.Valid;

import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.validation.BindingResult;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

import jp.co.nasuramsn.sitewarapper.app.form.EchoForm;

//Echo用Controller
@Controller
@RequestMapping("echo")
public class EchoController {

	@RequestMapping(method = RequestMethod.GET)
	public String viewInput(Model model) {
		EchoForm form = new EchoForm();
		model.addAttribute(form);
		return "echo/input";
	}

	@RequestMapping(method = RequestMethod.POST)
	public String echo(@Valid EchoForm echoForm, BindingResult result) {
		if (result.hasErrors()) {
			return "echo/input";
		} else {
			return "echo/output";
		}
	}
}
